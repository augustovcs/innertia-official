<script setup lang="ts">
import { ref } from 'vue';

import { useTheme } from '@/composables/useTheme';
import { Moon, Sun } from 'lucide-vue-next';

import Toggle from '@/components/ui/Toggle.vue';

const isToggled = ref<boolean>(false);

const { toggleTheme } = useTheme();
</script>

<template>
  <Toggle v-model="isToggled" @change="toggleTheme">
    <component
      :is="isToggled ? Sun : Moon"
      :class="isToggled ? 'text-amber-500' : 'text-zinc-500'"
    />
  </Toggle>
</template>
