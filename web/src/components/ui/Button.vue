<script setup lang="ts">
import { twMerge } from 'tailwind-merge';
import { computed, useAttrs } from 'vue';

interface ButtonProps {
  size?: 'sm' | 'md' | 'lg';
}

withDefaults(defineProps<ButtonProps>(), {
  size: 'md',
});

const attrs = useAttrs();

const className = computed(() => {
  const classAttr = attrs.class;
  return typeof classAttr === 'string' ? classAttr : undefined;
});

const defaultColors =
  'bg-primary text-primary-fg border border-transparent focus:ring-ring';
const defaultHoverColors = 'hover:bg-primary/80';

type Size = NonNullable<ButtonProps['size']>;

const sizeClasses: Record<Size, string[]> = {
  sm: ['px-3', 'py-1.5', 'text-sm', 'rounded-lg'],
  md: ['px-4', 'py-2', 'text-base', 'rounded-xl'],
  lg: ['px-6', 'py-2', 'text-md', 'font-medium', 'rounded-3xl'],
};
</script>

<template>
  <button
    v-bind="$attrs"
    :class="
      twMerge(
        'font-ibm-plex-sans transition-all duration-150 cursor-pointer',
        defaultColors,
        defaultHoverColors,
        sizeClasses[size],
        className,
      )
    "
  >
    <slot></slot>
  </button>
</template>
