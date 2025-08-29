import router from '@/routes';
import { defineStore } from 'pinia';

export type DashboardPages =
  | '/'
  | '/calendar'
  | '/tasks'
  | '/notes'
  | '/settings'
  | '/help-center';

const prefix = '/dashboard';

export const useDashboardStore = defineStore('dashboard', {
  state: (): { page: DashboardPages } => ({
    page: '/',
  }),
  actions: {
    current(current: DashboardPages) {
      this.page = current;
      router.push(prefix + current);
    },
  },
});
