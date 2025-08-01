import { createRouter, createWebHistory } from 'vue-router';

export default createRouter({
  history: createWebHistory(),
  routes: [
    {
      path: '/',
      component: () => import('@/views/Example.vue'),
      meta: {
        layout: 'default',
      },
    },
    {
      path: '/signup',
      component: () => import('@/views/SignUp.vue'),
      meta: {
        layout: 'default',
      },
    },
    {
      path: '/login',
      component: () => import('@/views/Login.vue'),
      meta: {
        layout: 'default',
      },
    },
  ],
});
