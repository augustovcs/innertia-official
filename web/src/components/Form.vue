<script setup lang="ts">
import { reactive } from 'vue';
import { type z } from 'zod';

import Button from '@/components/ui/Button.vue';
import Input from '@/components/ui/Input.vue';

export interface Field {
  name: string;
  label: string;
  type: 'text' | 'email' | 'password';
  placeholder?: string;
}

const props = defineProps<{
  fields: Field[];
  schema: z.ZodObject<any>;
  onSubmit: (data: any) => void;
}>();

const formData = reactive<Record<string, string>>({});
const errors = reactive<Record<string, string | undefined>>({});

props.fields.forEach(field => {
  formData[field.name] = '';
  errors[field.name] = undefined;
});

const validateField = (fieldName: string) => {
  const fieldSchema = props.schema.shape[fieldName];
  const result = fieldSchema.safeParse(formData[fieldName]);

  if (!result.success) {
    errors[fieldName] = result.error.issues[0].message;
  } else {
    errors[fieldName] = undefined;
  }
};

const validateAll = () => {
  const result = props.schema.safeParse(formData);

  Object.keys(errors).forEach(key => (errors[key] = undefined));

  if (!result.success) {
    for (const issue of result.error.issues) {
      errors[issue.path[0] as string] = issue.message;
    }
    return false;
  }

  return result.data;
};

const submit = () => {
  const data = validateAll();
  if (data) props.onSubmit(data);
};
</script>

<template>
  <form @submit.prevent="submit" class="flex flex-col gap-4">
    <div v-for="field in fields" :key="field.name" class="flex flex-col gap-1">
      <Input
        v-model="formData[field.name]"
        :id="field.name"
        :name="field.name"
        :type="field.type"
        :label="field.label"
        :error="errors[field.name]"
        @blur="() => validateField(field.name)"
      />
    </div>
    <Button type="submit">Send</Button>
  </form>
</template>
