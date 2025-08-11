<script setup lang="ts">
import { useToasterStore, type ToastStatus } from '@/stores/toaster';
import { twMerge } from 'tailwind-merge';

const iconUrl = (status: ToastStatus) => {
  return new URL(`../../assets/${status}.svg`, import.meta.url).href;
};

const statusClasses = {
  error: ['text-error'],
  warning: ['text-warning'],
  success: ['text-accent'],
};

const store = useToasterStore();
</script>

<template>
  <Teleport to="body">
    <Transition name="toast">
      <TransitionGroup
        class="fixed top-4 left-1/2 -translate-x-1/2 z-50 space-y-2"
        name="toast"
        enter-active-class="transition-all duration-300 ease-out"
        leave-active-class="transition-all duration-200 ease-out"
        enter-from-class="-translate-y-1/2 opacity-0"
        leave-to-class="-translate-y-1/2 opacity-0"
        tag="ul"
      >
        <li
          v-for="{ id, status, title } in store.toasts"
          :key="id"
          :class="['min-w-60 max-w-[30rem] min-h-12', 'rounded-xl bg-bg']"
        >
          <div
            :class="
              twMerge(
                'flex justify-start items-center gap-4 w-full',
                'w-full h-full',
                'px-5 py-4 rounded-xl',
                'bg-bg border-2 border-border/35 shadow-lg',
                statusClasses[status],
              )
            "
          >
            <img
              :src="iconUrl(status)"
              :alt="`toast icon ${status}`"
              class="size-5"
            />
            <h1
              class="text-base text-zinc-800 dark:text-zinc-100 font-ibm-plex-sans font-normal"
            >
              {{ title }}
            </h1>
          </div>
        </li>
      </TransitionGroup>
    </Transition>
  </Teleport>
</template>
