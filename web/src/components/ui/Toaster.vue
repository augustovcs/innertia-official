<script setup lang="ts">
import { useToasterStore, type Toast } from '@/stores/toaster';
import { CircleCheckBig, CircleX, TriangleAlert, X } from 'lucide-vue-next';
import { twMerge } from 'tailwind-merge';

const icons = {
  error: CircleX,
  warning: TriangleAlert,
  success: CircleCheckBig,
};

const statusClasses = {
  error: ['text-red-500', 'from-red-500/10'],
  warning: ['text-amber-600', 'from-amber-400/10'],
  success: ['text-emerald-500', 'from-emerald-500/10'],
};

const iconBgClasses = {
  error: 'bg-toast-icon-bg-error',
  warning: 'bg-toast-icon-bg-warning',
  success: 'bg-toast-icon-bg-success',
};

const store = useToasterStore();
const remove = ({ id }: Toast) => store.remove(id);
</script>

<template>
  <Teleport to="body">
    <Transition name="toast">
      <TransitionGroup
        class="fixed top-4 right-4 z-50 space-y-2"
        name="toast"
        enter-active-class="transition-all duration-300 ease-out"
        leave-active-class="transition-all duration-200 ease-out"
        enter-from-class="translate-x-1/2 opacity-0"
        leave-to-class="translate-x-1/2 opacity-0"
        tag="ul"
      >
        <li
          v-for="toast in store.toasts"
          :key="toast.id"
          :class="[
            'w-84 max-w-84 min-h-12',
            'rounded-xl bg-toast-bg overflow-hidden',
          ]"
        >
          <div
            :class="
              twMerge(
                'flex items-start gap-1.5 w-full',
                'w-full h-full',
                'bg-gradient-to-b to-toast-gradient-to border-2 border-toast-border px-4 pt-4 pb-6',
                statusClasses[toast.status],
              )
            "
          >
            <span
              :class="
                twMerge(
                  iconBgClasses[toast.status],
                  'flex items-start rounded-full p-1',
                )
              "
            >
              <component :is="icons[toast.status]" class="size-5" />
            </span>
            <div class="flex flex-col gap-2 w-full">
              <header class="flex justify-between items-start w-full">
                <h1 class="text-lg text-toast-title font-inter font-medium">
                  {{ toast.title }}
                </h1>
                <div class="flex items-start h-full">
                  <X @click="remove(toast)" class="text-toast-close-icon" />
                </div>
              </header>
              <p class="text-toast-description text-base">
                {{ toast.description }}
              </p>
            </div>
          </div>
        </li>
      </TransitionGroup>
    </Transition>
  </Teleport>
</template>
