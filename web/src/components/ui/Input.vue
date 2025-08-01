<script setup lang="ts">
import type { LucideProps } from 'lucide-vue-next';
import { twMerge } from 'tailwind-merge';
import { computed, type FunctionalComponent, useAttrs, useSlots } from 'vue';

const model = defineModel<any>();
const emit = defineEmits<{
  (e: 'blur', event: FocusEvent): void;
}>();

interface Props {
  placeholder?: string;
  error?: string;
  icon?: FunctionalComponent<LucideProps, {}, any, {}>;
  iconPosition?: 'left' | 'right';
  size?: 'sm' | 'md' | 'lg';
}

const props = withDefaults(defineProps<Props>(), {
  iconPosition: 'left',
  size: 'md',
});

const attrs = useAttrs();
const slots = useSlots();

const hasIcon = () => !!slots.default;

const id = computed(() => attrs.id as string | undefined);

const sizeClasses = {
  sm: ['text-xs', 'px-3', 'py-1.5'],
  md: ['text-sm', 'px-4', 'py-2'],
  lg: ['text-sm', 'px-5', '2xl:py-4 py-3'],
};

const baseInputClass = computed(() =>
  twMerge(
    'font-inter font-medium text-primary',
    'rounded-3xl border-[0.14rem] border-border bg-transparent',
    'focus:outline-none focus:ring-2 peer',
    props.error
      ? 'border-red-400 focus:ring-zinc-50/20'
      : 'focus:ring-focus-ring',
    sizeClasses[props.size],
    hasIcon() ? (props.iconPosition === 'left' ? 'pl-12' : 'pr-12') : 'pl-4',
  ),
);

const iconClass = computed(() =>
  twMerge(
    'absolute top-1/2 -translate-y-1/2 text-primary dark:text-border',
    props.error ? 'text-red-400' : '',
    props.iconPosition === 'left' ? 'left-3' : 'right-3',
  ),
);
</script>

<template>
  <div class="flex flex-col gap-1 w-full">
    <div class="relative w-full">
      <span :class="iconClass">
        <slot class="stroke-[2.2px] size-0.5" />
      </span>

      <input
        v-bind="attrs"
        v-model="model"
        :placeholder="placeholder"
        :id="id"
        :class="baseInputClass"
        @blur="emit('blur', $event)"
      />
    </div>

    <span
      :class="[
        error ? 'opacity-100' : 'opacity-0',
        'h-2',
        'text-xs text-red-400 font-ubuntu font-medium',
      ]"
    >
      {{ error }}
    </span>
  </div>
</template>
