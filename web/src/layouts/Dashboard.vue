<script setup lang="ts">
import Sidebar from '@/components/pages/dashboard/Sidebar.vue';
</script>

<template>
  <div class="bg-bg flex-1 w-full min-h-screen flex gap-4 overflow-y-auto">
    <Sidebar />
    <main class="flex flex-col w-full h-screen p-8 overflow-y-scroll">
      <slot></slot>
    </main>
  </div>
</template>
