<script setup lang="ts">
import { useModalStore } from '@/stores/modal';
import { X } from 'lucide-vue-next';
import { computed } from 'vue';

const props = defineProps<{
  name: string;
}>();

const modalStore = useModalStore();

modalStore.create(props.name);

const isOpen = computed(() => modalStore.modals[props.name]?.isOpen ?? false);

const close = () => {
  modalStore.close(props.name);
};
</script>

<template>
  <Teleport to="body">
    <div
      v-if="isOpen"
      class="fixed inset-0 bg-modal-overlay-bg flex items-center justify-center"
    >
      <div
        class="bg-modal-bg m-2 rounded-lg shadow-lg relative min-w-[32rem] max-w-[64rem] min-h-[28rem] overflow-hidden"
      >
        <button
          @click="close"
          class="cursor-pointer absolute top-4 right-4 text-modal-close-icon hover:text-modal-close-icon-hover"
        >
          <X />
        </button>
        <slot />
      </div>
    </div>
  </Teleport>
</template>
