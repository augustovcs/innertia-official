<script lang="ts">
import { computed } from 'vue';
import { useRoute } from 'vue-router';

export default {
  setup() {
    const defaultLayout = 'default';

    const route = useRoute();

    const layout = computed(
      () => `${route.meta.layout || defaultLayout}-layout`,
    );

    return {
      layout,
    };
  },
};
</script>

<template>
  <component :is="layout">
    <RouterView />
  </component>
</template>
