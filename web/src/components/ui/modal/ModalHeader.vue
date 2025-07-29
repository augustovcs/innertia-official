<script setup lang="ts">
interface Props {
  title: string;
  label?: string;
}

const props = defineProps<Props>();
</script>

<template>
  <header
    class="flex flex-col gap-1 px-6 py-4 border-b border-modal-border bg-modal-header-bg"
  >
    <h1
      class="font-inter text-[1.5rem] font-semibold text-modal-title leading-tight tracking-tight"
    >
      {{ props.title }}
    </h1>
    <p v-show="props.label" class="text-sm text-modal-description">
      {{ props.label }}
    </p>
  </header>
</template>
