<script setup lang="ts">
import LogoIcon from '@/components/LogoIcon.vue';
import Button from '@/components/ui/Button.vue';
import Checkbox from '@/components/ui/Checkbox.vue';
import Input from '@/components/ui/Input.vue';
import Label from '@/components/ui/Label.vue';
import Toaster from '@/components/ui/Toaster.vue';
import { useToast } from '@/composables/useToast';
import { signUp, signUpSchema, type TSignUpSchema } from '@/services/auth/signUp';
import { AxiosError, HttpStatusCode } from 'axios';
import { Eye, EyeClosed, LoaderCircle } from 'lucide-vue-next';
import { reactive, ref } from 'vue';

const toast = useToast();

const formSchema = signUpSchema.shape;

const loading = ref<boolean>(false);
const showPassword = ref<boolean>(false);
const showPasswordConfirmation = ref<boolean>(false);

const formData = reactive<Record<keyof TSignUpSchema, string | boolean | undefined>>({
  fullname: undefined,
  email: undefined,
  password: undefined,
  confirmPassword: undefined,
	termsAccepted: false,
});
const formErrors = reactive<Record<keyof TSignUpSchema, string | undefined>>({
  fullname: undefined,
  email: undefined,
  password: undefined,
  confirmPassword: undefined,
	termsAccepted: undefined,
});

const validateFormField = (fieldName: keyof TSignUpSchema) => {
  if (fieldName === 'confirmPassword') {
    const match = formData['password'] === formData['confirmPassword'];

    formErrors['password'] = match ? undefined : 'As senhas não coincidem';
    formErrors['confirmPassword'] = match
      ? undefined
      : 'As senhas não coincidem';

    return;
  }

  const field = formSchema[fieldName];
  const parsed = field.safeParse(formData[fieldName]);

  if (!parsed.success) {
    formErrors[fieldName] = parsed.error.issues[0].message;
  } else {
    formErrors[fieldName] = undefined;
  }
};

const validateFormData = (): TSignUpSchema | undefined => {
  const { success, error, data } = signUpSchema.safeParse(formData);

  Object.keys(formErrors).forEach(key => {
    formErrors[key as keyof TSignUpSchema] = undefined;
  });

  if (!success) {
    for (const issue of error.issues) {
      formErrors[issue.path[0] as keyof TSignUpSchema] = issue.message;
    }
    return;
  }

  return data;
};

const submit = async () => {
	loading.value = true;
  const user = validateFormData();

  if (!user) {
		loading.value = false;
    return;
	}

	const { email, password } = user;

	try {
		const { data } = await signUp({ email, password_hash: password });

    loading.value = false;

		// TODO: add session
		console.log(data);
		document.location.href = document.location.origin + '/dashboard';
	} catch (err) {
		loading.value = false;

		if (err instanceof AxiosError) {
			const { status } = err;

      if (!status) {
				toast.error('Ops! Alguma deu errado')
				return;
			};

      const clientErrors = [HttpStatusCode.BadRequest, HttpStatusCode.NotFound]

			if (clientErrors.includes(status)) {
				toast.error('Email ou senha incorretos')
				return;
			}
		}
		toast.error('Ops! Alguma deu errado')
	}
};

const imgUrl =
  'https://plus.unsplash.com/premium_photo-1668383207188-f5474588d674?q=80&w=687&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D';
</script>

<template>
  <div
    class="flex flex-col-reverse justify-between items-center lg:flex-row xl:gap-0 w-full max-h-screen min-h-screen lg:p-4 xl:p-6"
  >
    <div
      class="flex-1 lg:flex-1/2 xl:max-h-screen xl:min-h-screen flex flex-col justify-evenly xl:justify-normal xl:gap-y-8 rounded-t-2xl lg:px-12 xl:px-24 xl:pt-8 2xl:py-40"
    >
      <div
        class="absolute z-10 top-0 xs:top-10 md:top-20 left-6 lg:static flex flex-col mb-6"
      >
        <LogoIcon height="80" width="128" class="text-bg/95 lg:text-fg" />
        <h1
          class="font-montserrat font-semibold text-bg/95 lg:text-fg text-2xl xs:text-5xl 2xl:mt-12"
        >
          Vamos lá
        </h1>
        <p
          class="font-ibm-plex-sans font-normal text-bg/95 lg:text-primary text-sm"
        >
          Faça o registro e dê o proximo passo
        </p>
      </div>

      <Button
        class="w-[90wm] lg:full flex justify-center items-center gap-x-6 bg-neutral/10 hover:bg-neutral/20 border-2 border-border"
      >
        <img src="@/assets/google.svg" alt="google icon" class="size-8" />
        <label class="text-primary text-lg font-ibm-plex-sans font-medium"
          >Fazer login com Google</label
        >
      </Button>

      <div class="flex items-center gap-2">
        <hr class="w-full text-border" />
        <p class="font-ibm-plex-sans text-primary">Ou</p>
        <hr class="w-full text-border" />
      </div>

      <div class="w-[90vw] lg:w-full">
        <!-- Form -->
        <form @submit.prevent="submit" class="flex flex-col gap-4">
          <div>
            <Label class="hidden xs:block" text="Fullname" for="fullname" />
            <Input
              v-model="formData['fullname']"
              id="fullname"
              size="lg"
              placeholder="Digite seu nome completo"
              class="w-full"
              @blur="validateFormField('fullname')"
              :error="formErrors['fullname']"
            />
          </div>
          <div>
            <Label class="hidden xs:block" text="Email" for="email" />
            <Input
              v-model="formData['email']"
              id="email"
              size="lg"
              placeholder="Digite seu email"
              class="w-full"
              @blur="validateFormField('email')"
              :error="formErrors['email']"
            />
          </div>
          <div class="w-full flex flex-col xl:flex-row gap-2">
            <div class="flex-1">
              <Label class="hidden xs:block" text="Password" for="password" />
              <Input
                v-model="formData['password']"
                id="password"
                size="lg"
                :type="showPassword ? 'text' : 'password'"
                placeholder="Digite sua senha"
                class="w-full"
                icon-position="right"
                @blur="validateFormField('password')"
                :error="formErrors['password']"
              >
                <span
                  class="cursor-pointer"
                  @click="() => (showPassword = !showPassword)"
                >
                  <Eye class="size-5" v-if="showPassword" />
                  <EyeClosed class="size-5" v-else />
                </span>
              </Input>
            </div>
            <div class="flex-1">
              <Label
                class="hidden xs:block"
                text="Confirmar senha"
                for="confirmPassword"
              />
              <Input
                v-model="formData['confirmPassword']"
                id="confirmPassword"
                size="lg"
                :type="showPasswordConfirmation ? 'text' : 'password'"
                placeholder="Confirme sua senha"
                class="w-full"
                icon-position="right"
                @blur="validateFormField('confirmPassword')"
                :error="formErrors['confirmPassword']"
              >
                <span
                  class="cursor-pointer"
                  @click="
                    () => (showPasswordConfirmation = !showPasswordConfirmation)
                  "
                >
                  <Eye class="size-5" v-if="showPasswordConfirmation" />
                  <EyeClosed class="size-5" v-else />
                </span>
              </Input>
            </div>
          </div>
          <Checkbox v-model="formData['termsAccepted']" name="termPolicy" value="accept">
            <label class="text-sm xl:text-base">
              Concordo com os
              <a href="#" class="text-link hover:underline"
                >Termos de Política e Privacidade</a
              >
            </label>
          </Checkbox>
          <Button type="submit" size="lg" :disabled="loading" class="flex justify-center items-center w-full h-12 disabled:opacity-50">
						<LoaderCircle v-if="loading" class="animate-spin"/>
						<p v-else>Registrar conta</p>
					</Button>
					<Toaster />
        </form>
        <p class="mt-2 font-ibm-plex-sans text-sm xl:text-base">
          Já tem uma conta?
          <a href="/login" class="text-link hover:underline">Fazer o login</a>
        </p>
      </div>
    </div>
    <div
      class="max-h-screen w-screen lg:w-[50vw] bg-fg lg:bg-transparent overflow-hidden"
    >
      <img
        class="h-[30vh] md:h-[40vh] lg:h-[95vh] w-full object-cover opacity-50 lg:opacity-100 lg:rounded-2xl"
        :src="imgUrl"
        alt="productivity ilustrate"
      />
    </div>
  </div>
</template>
