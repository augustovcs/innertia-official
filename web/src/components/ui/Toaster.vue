<script setup lang="ts">
import { useToasterStore } from '@/stores/toaster';
import { CircleCheckBig, CircleX, TriangleAlert } from 'lucide-vue-next';
import { twMerge } from 'tailwind-merge';

const icons = {
  error: CircleX,
  warning: TriangleAlert,
  success: CircleCheckBig,
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
          v-for="toast in store.toasts"
          :key="toast.id"
          :class="['w-60 max-w-60 min-h-12', 'rounded-xl bg-bg']"
        >
          <div
            :class="
              twMerge(
                'flex justify-start items-center gap-4 w-full',
                'w-full h-full',
                'px-5 py-4 rounded-xl',
                'bg-bg border-2 border-border/35 shadow-lg',
                statusClasses[toast.status],
              )
            "
          >
            <component :is="icons[toast.status]" class="size-5" />
            <h1
              class="text-lg text-zinc-800 dark:text-zinc-100 font-ibm-plex-sans font-normal"
            >
              {{ toast.title }}
            </h1>
          </div>
        </li>
      </TransitionGroup>
    </Transition>
  </Teleport>
</template>
