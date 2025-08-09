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
    {
      path: '/dashboard',
      component: () => import('@/views/dashboard/index.vue'),
      meta: {
        layout: 'dashboard',
      },
    },
    {
      path: '/dashboard/calendar',
      component: () => import('@/views/dashboard/Calendar.vue'),
      meta: {
        layout: 'dashboard',
      },
    },
    {
      path: '/dashboard/tasks',
      component: () => import('@/views/dashboard/Tasks.vue'),
      meta: {
        layout: 'dashboard',
      },
    },
    {
      path: '/dashboard/notes',
      component: () => import('@/views/dashboard/Notes.vue'),
      meta: {
        layout: 'dashboard',
      },
    },
    {
      path: '/dashboard/settings',
      component: () => import('@/views/dashboard/Settings.vue'),
      meta: {
        layout: 'dashboard',
      },
    },
  ],
});
