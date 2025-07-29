<script setup lang="ts">
import { computed } from 'vue';

interface ToggleProps {
  modelValue: boolean;
  iconOn?: string;
  iconOff?: string;
  disabled?: boolean;
}

const props = withDefaults(defineProps<ToggleProps>(), {
  iconOn: undefined,
  iconOff: undefined,
  disabled: false,
});

const emit = defineEmits<{
  (e: 'update:modelValue', value: boolean): void;
}>();

const isIconMode = computed(() => !!props.iconOn && !!props.iconOff);

const toggleClasses = computed(() => [
  'relative inline-flex items-center h-6 w-11 rounded-full transition-colors duration-200 ease-in-out',
  props.modelValue ? 'bg-primary' : 'bg-foreground/30',
  props.disabled ? 'opacity-50 cursor-not-allowed' : 'cursor-pointer',
]);

const sliderClasses = computed(() => [
  'absolute left-1 h-4 w-4 rounded-full bg-background transition-transform duration-200 ease-in-out',
  props.modelValue ? 'translate-x-5' : 'translate-x-0',
]);

const iconClasses = computed(() => ({
  on: [
    props.iconOn,
    'h-5 w-5',
    props.modelValue ? 'opacity-100' : 'opacity-0',
    'transition-opacity duration-200',
  ],
  off: [
    props.iconOff,
    'h-5 w-5',
    props.modelValue ? 'opacity-0' : 'opacity-100',
    'transition-opacity duration-200',
  ],
}));
</script>

<template>
  <label
    class="inline-flex items-center gap-2"
    :class="{ 'cursor-not-allowed': disabled, 'cursor-pointer': !disabled }"
  >
    <slot></slot>
    <input
      type="checkbox"
      :checked="modelValue"
      :disabled="disabled"
      class="sr-only"
      @input="
        emit('update:modelValue', ($event.target as HTMLInputElement).checked)
      "
    />
    <span :class="toggleClasses">
      <span v-if="isIconMode" :class="iconClasses.on"></span>
      <span v-if="isIconMode" :class="iconClasses.off"></span>
      <span v-else :class="sliderClasses"></span>
    </span>
  </label>
</template>
