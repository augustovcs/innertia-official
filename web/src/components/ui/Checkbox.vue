<script setup lang="ts">
const model = defineModel<any>();

interface CheckboxProps {
  name: string;
  value: string;
}

defineProps<CheckboxProps>();
</script>

<template>
  <div class="flex items-center gap-2">
    <input
      type="checkbox"
      id="checkbox"
      :name="name"
      :value="value"
      v-model="model"
      class="size-5 accent-primary cursor-pointer"
    />
    <label for="checkbox" class="text-primary font-ibm-plex-sans">
      <slot />
    </label>
  </div>
</template>
