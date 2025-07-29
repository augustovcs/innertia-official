<script setup lang="ts">
import { twMerge } from 'tailwind-merge';
import { computed, useAttrs } from 'vue';

interface ButtonProps {
  variant?: 'primary' | 'secondary' | 'outline';
  size?: 'sm' | 'md' | 'lg';
  color?: string;
  hoverColor?: string;
}

const props = withDefaults(defineProps<ButtonProps>(), {
  variant: 'primary',
  size: 'md',
  color: undefined,
  hoverColor: undefined,
});

const attrs = useAttrs();

const className = computed(() => {
  const classAttr = attrs.class;
  return typeof classAttr === 'string' ? classAttr : undefined;
});

type Variant = NonNullable<ButtonProps['variant']>;

const defaultColors: Record<Variant, string> = {
  primary: 'bg-primary',
  secondary: 'bg-secondary',
  outline: 'bg-transparent',
};

// Mapeia cores de hover padrão por variante
const defaultHoverColors: Record<Variant, string> = {
  primary: 'hover:bg-primary/90',
  secondary: 'hover:bg-secondary/70',
  outline: 'hover:bg-accent',
};

const variantClasses = computed(() => {
  const baseClasses: Record<Variant, string[]> = {
    primary: [
      'text-primary-foreground',
      'border',
      'border-transparent',
      'focus:ring-ring',
    ],
    secondary: [
      'text-foreground',
      'border',
      'border-transparent',
      'focus:ring-secondary',
    ],
    outline: ['text-primary', 'border', 'border-primary', 'focus:ring-ring'],
  };

  const colorClass = props.color ?? defaultColors[props.variant];
  const hoverClass = props.hoverColor ?? defaultHoverColors[props.variant];

  return [...baseClasses[props.variant], colorClass, hoverClass];
});

type Size = NonNullable<ButtonProps['size']>;

const sizeClasses: Record<Size, string[]> = {
  sm: ['px-3', 'py-1.5', 'text-sm', 'rounded-lg'],
  md: ['px-4', 'py-2', 'text-base', 'rounded-xl'],
  lg: ['px-6', 'py-3', 'text-lg', 'rounded-2xl'],
};
</script>

<template>
  <button
    v-bind="$attrs"
    :class="
      twMerge(
        className,
        variantClasses,
        sizeClasses[size],
        'font-inter transition-all duration-150 cursor-pointer',
      )
    "
  >
    <slot></slot>
  </button>
</template>
