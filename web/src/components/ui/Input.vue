<script setup lang="ts">
import type { LucideProps } from 'lucide-vue-next';
import { twMerge } from 'tailwind-merge';
import { computed, type FunctionalComponent, useAttrs } from 'vue';

import Label from '@/components/ui/Label.vue';

const model = defineModel<any>();
const emit = defineEmits<{
  (e: 'blur', event: FocusEvent): void;
}>();

interface Props {
  label?: string;
  error?: string;
  icon?: FunctionalComponent<LucideProps, {}, any, {}>;
  iconPosition?: 'left' | 'right';
  size?: 'sm' | 'md' | 'lg';
}

const props = withDefaults(defineProps<Props>(), {
  labelInside: false,
  iconPosition: 'left',
  size: 'md',
});

const attrs = useAttrs();

const id = computed(() => attrs.id as string | undefined);

const hasValue = computed(() => !!model.value);

const sizeClasses = {
  sm: ['text-sm', 'px-3', 'py-1.5'],
  md: ['text-base', 'px-4', 'py-3'],
  lg: ['text-md', 'px-5', 'py-4'],
};

const baseInputClass = computed(() =>
  twMerge(
    'font-inter font-medium text-input-text',
    'rounded-2xl border-[0.14rem] border-input-border/50 bg-transparent',
    'focus:outline-none focus:ring-2 peer',
    props.error
      ? 'border-red-400 focus:ring-zinc-50/20'
      : 'focus:ring-focus-ring',
    sizeClasses[props.size],
    props.icon ? (props.iconPosition === 'left' ? 'pl-12' : 'pr-12') : 'pl-4',
  ),
);

const labelClass = computed(() => {
  const floatEffect =
    props.size !== 'sm'
      ? [
          '-translate-y-4 scale-75',
          'peer-placeholder-shown:translate-y-0',
          'peer-placeholder-shown:scale-100',
          'peer-focus:-translate-y-4',
          'peer-focus:scale-75',
          'rtl:peer-focus:translate-x-1/4',
          'rtl:peer-focus:left-auto',
        ]
      : ['transition-all duration-150 peer-focus:opacity-0'];

  return twMerge(
    'absolute z-10 start-2.5 origin-[0]',
    'transition-transform duration-300 transform',
    props.size === 'sm' ? 'top-2' : 'top-[30%]',
    props.icon ? (props.iconPosition === 'left' ? 'pl-12' : 'pr-12') : 'pl-2',
    ...floatEffect,
  );
});

const iconClass = computed(() =>
  twMerge(
    'absolute top-1/2 -translate-y-1/2 text-label-text pointer-events-none',
    props.error ? 'text-red-400' : '',
    props.iconPosition === 'left' ? 'left-3' : 'right-3',
  ),
);
</script>

<template>
  <div class="flex flex-col gap-1 w-full">
    <div class="relative w-full">
      <span v-if="icon" :class="iconClass">
        <component :is="icon" class="stroke-[2.2px] w-5 h-5" />
      </span>

      <div class="relative">
        <input
          v-bind="attrs"
          v-model="model"
          placeholder=" "
          :id="id"
          :class="baseInputClass"
          @blur="emit('blur', $event)"
        />

        <Label
          v-if="label"
          :text="label"
          :for="id"
          :class="[labelClass, { 'opacity-0': hasValue }]"
        />
      </div>
    </div>

    <span v-if="error" class="text-xs text-red-400 font-inter font-medium">
      {{ error }}
    </span>
  </div>
</template>
