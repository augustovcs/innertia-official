<script setup lang="ts">
import { twMerge } from 'tailwind-merge';
import { computed, useAttrs } from 'vue';

const attrs = useAttrs();

const className = computed(() => attrs.class as string | undefined);
</script>

<template>
  <div :class="twMerge('animate-pulse rounded-lg bg-skeleton', className)" />
</template>
