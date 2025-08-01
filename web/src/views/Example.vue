<script setup lang="ts">
import { AlertCircle, Mail, Search, User } from 'lucide-vue-next';
import { onMounted, ref } from 'vue';

import { useToast } from '@/composables/useToast';
import { useModalStore } from '@/stores/modal';

import Button from '@/components/ui/Button.vue';
import Input from '@/components/ui/Input.vue';
import Modal from '@/components/ui/modal/Modal.vue';
import ModalHeader from '@/components/ui/modal/ModalHeader.vue';
import Skeleton from '@/components/ui/Skeleton.vue';
import Toaster from '@/components/ui/Toaster.vue';
import ToggleTheme from '@/components/ui/ToggleTheme.vue';

// Stores and Composables
const modalStore = useModalStore();
const toast = useToast();

// UI State
const loading = ref(true);
const loadTime = 5000;

// Modal
const openModal = () => modalStore.open('modal');

// Toasts
const successToast = () =>
  toast.success('All right!', 'You can continue without any problem');

const warningToast = () =>
  toast.warning('Warning!', 'Something might be wrong, stay calm');

const errorToast = () =>
  toast.error(
    'Ops! This is broken',
    'This is an internal error. Don’t worry, we’ll resolve it.',
  );

// Simulate async loading (e.g. skeleton loading)
onMounted(() => {
  setTimeout(() => {
    loading.value = false;
  }, loadTime);
});
</script>

<template>
  <div class="flex flex-col gap-8 p-2">
    <!-- Buttons -->
    <section class="flex flex-col gap-2 w-fit">
      <h1>Buttons:</h1>
      <Button size="lg">Button 1</Button>
      <Button size="md">Button 2</Button>
      <Button size="sm">Button 3</Button>
    </section>

    <!-- Inputs -->
    <section class="flex flex-col gap-2 w-fit">
      <h1>Inputs:</h1>
      <Input label="Email" size="lg">
        <Mail />
      </Input>
      <Input label="Username" :icon="User" size="md">
        <User />
      </Input>
      <Input label="Search" :icon="Search" size="sm">
        <Search />
      </Input>
      <Input
        label="Mail"
        :icon="AlertCircle"
        size="sm"
        error="You did not fill this correctly"
      >
        <AlertCircle />
      </Input>
    </section>

    <!-- Modal -->
    <section class="flex flex-col gap-2 w-fit">
      <h1>Modal:</h1>
      <Modal name="modal">
        <ModalHeader
          title="Modal"
          label="Excepteur sint occaecat cupidatat non proident"
        />
      </Modal>
      <Button @click="openModal">Open</Button>
    </section>

    <!-- Toasts -->
    <section class="flex flex-col gap-2 w-fit">
      <Toaster />
      <Button
        @click="successToast"
        variant="outline"
        size="lg"
        class="bg-emerald-400/20 text-emerald-400 border-emerald-400"
        hover-color="hover:bg-emerald-50/10"
      >
        Success
      </Button>
      <Button
        @click="warningToast"
        variant="outline"
        size="lg"
        class="bg-amber-400/20 text-amber-400 border-amber-400"
        hover-color="hover:bg-amber-50/10"
      >
        Warning
      </Button>
      <Button
        @click="errorToast"
        variant="outline"
        size="lg"
        class="bg-red-400/20 text-red-400 border-red-400"
        hover-color="hover:bg-red-50/10"
      >
        Error
      </Button>
    </section>

    <!-- Skeleton -->
    <section>
      <h1>Skeletons:</h1>
      <div class="flex flex-wrap gap-4 justify-between w-full">
        <div
          v-for="example in [1, 2, 3, 4, 5]"
          :key="example"
          class="flex flex-col gap-4 w-1/5"
        >
          <template v-if="loading">
            <Skeleton class="w-full h-56" />
            <Skeleton class="w-full h-12" />
          </template>
          <template v-else>
            <div
              class="rounded-lg bg-zinc-300 w-full h-56 flex justify-center items-center"
            >
              <h1>Fake content</h1>
            </div>
            <div
              class="rounded-lg bg-zinc-300 w-full h-12 flex justify-center items-center"
            >
              <p>Fake description</p>
            </div>
          </template>
        </div>
      </div>
    </section>

    <!-- Theme Toggle -->
    <ToggleTheme />
  </div>
</template>
