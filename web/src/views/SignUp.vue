<script setup lang="ts">
import LogoIcon from '@/components/LogoIcon.vue';
import Button from '@/components/ui/Button.vue';
import Checkbox from '@/components/ui/Checkbox.vue';
import Input from '@/components/ui/Input.vue';
import Label from '@/components/ui/Label.vue';
import { Eye, EyeClosed } from 'lucide-vue-next';
import { reactive, ref } from 'vue';

import { z } from 'zod';

const FormSchema = z
  .object({
    fullname: z
      .string('Insira o seu nome')
      .refine(
        val => val.trim().split(' ').length >= 2,
        'Insira o nome completo',
      ),
    email: z.email('Insira um email válido'),
    password: z
      .string('Campo obrigatório')
      .min(6, 'A senha deve conter pelo menos 6 caracteres'),
    confirmPassword: z.string('As senhas não coincidem'),
  })
  .superRefine(({ password, confirmPassword }, ctx) => {
    if (password !== confirmPassword) {
      ctx.addIssue({
        code: 'custom',
        message: 'As senhas não coincidem',
        path: ['confirmPassword'],
      });
    }
  });

type Form = z.infer<typeof FormSchema>;

const formSchema = FormSchema.shape;
const showPassword = ref<boolean>(false);
const showPasswordConfirmation = ref<boolean>(false);
const termsAccepted = ref<boolean>(false);

const formData = reactive<Record<keyof Form, string | undefined>>({
  fullname: undefined,
  email: undefined,
  password: undefined,
  confirmPassword: undefined,
});
const formErrors = reactive<Record<keyof Form, string | undefined>>({
  fullname: undefined,
  email: undefined,
  password: undefined,
  confirmPassword: undefined,
});

const validateFormField = (fieldName: keyof Form) => {
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

const validateFormData = (): z.infer<typeof FormSchema> | undefined => {
  if (!termsAccepted) {
    return;
  }

  const result = FormSchema.safeParse(formData);

  Object.keys(formErrors).forEach(key => {
    formErrors[key as keyof Form] = undefined;
  });

  if (!result.success) {
    for (const issue of result.error.issues) {
      formErrors[issue.path[0] as keyof Form] = issue.message;
    }
    return;
  }

  return result.data;
};

const submit = () => {
  const data = validateFormData();
  console.log(termsAccepted.value);
  if (data) console.log(data);
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
          <Checkbox v-model="termsAccepted" name="termPolicy" value="accept">
            <label class="text-sm xl:text-base">
              Concordo com os
              <a href="#" class="text-link hover:underline"
                >Termos de Política e Privacidade</a
              >
            </label>
          </Checkbox>
          <Button type="submit" size="lg" class="w-full h-12"
            >Registrar conta</Button
          >
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
