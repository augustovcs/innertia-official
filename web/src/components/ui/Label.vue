<script setup lang="ts">
import { twMerge } from 'tailwind-merge';
import { computed, useAttrs } from 'vue';

interface Props {
  for?: string;
  text: string;
  description?: string;
  required?: boolean;
}

const props = defineProps<Props>();

const attrs = useAttrs();

const className = computed(() => attrs.class as string | undefined);
</script>

<template>
  <label
    :for="props.for"
    :class="
      twMerge(
        'flex flex-col gap-0.5',
        'transition-opacity duration-300',
        className,
      )
    "
  >
    <span class="text-sm font-medium text-label-text">
      {{ text }}
      <span v-if="required" class="text-red-500 ml-0.5">*</span>
    </span>
    <span v-if="description" class="text-xs text-zinc-500">
      {{ description }}
    </span>
  </label>
</template>
