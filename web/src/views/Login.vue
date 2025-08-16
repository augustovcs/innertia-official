<script setup lang="ts">
import LogoIcon from '@/components/LogoIcon.vue';
import Button from '@/components/ui/Button.vue';
import Input from '@/components/ui/Input.vue';
import Label from '@/components/ui/Label.vue';
import Toaster from '@/components/ui/Toaster.vue';
import { useToast } from '@/composables/useToast';
import { login, loginSchema, type TLoginSchema } from '@/services/auth/login';
import { setCookie, useCookies } from '@/utils/cookies';
import { AxiosError, HttpStatusCode } from 'axios';
import { Eye, EyeClosed, LoaderCircle } from 'lucide-vue-next';
import { reactive, ref } from 'vue';

useCookies();

const toast = useToast();

const formSchema = loginSchema.shape;

const loading = ref<boolean>(false);
const showPassword = ref<boolean>(false);

const formData = reactive<Record<keyof TLoginSchema, string | undefined>>({
  email: undefined,
  password: undefined,
});
const formErrors = reactive<Record<keyof TLoginSchema, string | undefined>>({
  email: undefined,
  password: undefined,
});

const validateFormField = (fieldName: keyof TLoginSchema) => {
  const field = formSchema[fieldName];
  const parsed = field.safeParse(formData[fieldName]);

  if (!parsed.success) {
    formErrors[fieldName] = parsed.error.issues[0].message;
  } else {
    formErrors[fieldName] = undefined;
  }
};

const validateFormData = () => {
  const { success, error, data } = loginSchema.safeParse(formData);

  Object.keys(formErrors).forEach(key => {
    formErrors[key as keyof TLoginSchema] = undefined;
  });

  if (!success) {
    for (const issue of error.issues) {
      formErrors[issue.path[0] as keyof TLoginSchema] = issue.message;
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
    const { data } = await login({ email, password });

    const { token } = data;
		setCookie("session_token", token);

    loading.value = false;

    document.location.href = document.location.origin + '/dashboard';
  } catch (err) {
    loading.value = false;

    if (err instanceof AxiosError) {
      const { status } = err;

      if (!status) {
        toast.error('Ops! Alguma deu errado');
        return;
      }

      const clientErrors = [HttpStatusCode.BadRequest, HttpStatusCode.NotFound];

      if (clientErrors.includes(status)) {
        toast.error('Email ou senha incorretos');
        return;
      }
    }
    toast.error('Ops! Alguma deu errado');
  }
};

const imgUrl =
  'https://images.unsplash.com/photo-1612202303082-b1161ab02548?q=80&w=736&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D';
</script>

<template>
  <div
    class="flex flex-col justify-between items-center lg:flex-row xl:gap-0 w-full max-h-screen min-h-screen lg:p-4 xl:p-6"
  >
    <div
      class="max-h-screen w-screen lg:w-[50vw] bg-fg lg:bg-transparent overflow-hidden"
    >
      <img
        class="h-[30vh] md:h-[40vh] lg:h-[95vh] w-full object-cover opacity-50 lg:opacity-100 lg:rounded-2xl"
        :src="imgUrl"
        alt="productivity ilustrate"
      />
    </div>
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
          Bem-vindo(a) de volta!
        </h1>
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
          </div>
          <Button
            type="submit"
            size="lg"
            :disabled="loading"
            class="flex justify-center items-center w-full h-12 disabled:opacity-50"
          >
            <LoaderCircle v-if="loading" class="animate-spin" />
            <p v-else>Entrar</p>
          </Button>
          <Toaster />
        </form>
        <p class="mt-2 font-ibm-plex-sans text-sm xl:text-base">
          Não tem uma conta?
          <a href="/signup" class="text-link hover:underline">Registre-se</a>
        </p>
      </div>
    </div>
  </div>
</template>
