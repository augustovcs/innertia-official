import { createRouter, createWebHistory } from 'vue-router';
import { isAuthenticated } from './middlewares';

export default createRouter({
  history: createWebHistory(),
  routes: [
    {
      path: '/',
      component: () => import('@/views/Example.vue'),
      meta: {
				auth: false,
        layout: 'default',
      },
    },
    {
      path: '/signup',
			beforeEnter: (_to, _from, next) => isAuthenticated('not-authenticated', next),
      component: () => import('@/views/SignUp.vue'),
      meta: {
				auth: false,
        layout: 'default',
      },
    },
    {
      path: '/login',
			beforeEnter: (_to, _from, next) => isAuthenticated('not-authenticated', next),
      component: () => import('@/views/Login.vue'),
      meta: {
				auth: false,
        layout: 'default',
      },
    },
    {
      path: '/dashboard',
			beforeEnter: (_to, _from, next) => isAuthenticated('authenticated', next),
      component: () => import('@/views/dashboard/index.vue'),
      meta: {
        layout: 'dashboard',
      },
      children: [
        {
          path: 'calendar',
          component: () => import('@/views/dashboard/Calendar.vue'),
        },
        {
          path: 'tasks',
          component: () => import('@/views/dashboard/Tasks.vue'),
        },
        {
          path: 'notes',
          component: () => import('@/views/dashboard/Notes.vue'),
        },
        {
          path: 'settings',
          component: () => import('@/views/dashboard/Settings.vue'),
        },
      ],
    },
  ],
});
