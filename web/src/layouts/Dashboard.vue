<script setup lang="ts">
import Sidebar from '@/components/pages/dashboard/Sidebar.vue';
</script>

<template>
  <div class="flex-1 w-full min-h-screen flex gap-4 overflow-hidden">
    <Sidebar />
    <main class="flex flex-col w-full h-full p-8 overflow-y-auto">
      <slot></slot>
    </main>
  </div>
</template>
