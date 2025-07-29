import { defineStore } from 'pinia';

export type ToastStatus = 'success' | 'warning' | 'error';

export interface Toast {
  id: string;
  title: string;
  description: string;
  status: ToastStatus;
}

const defaultTimeout = 5000;

export const useToasterStore = defineStore('toaster', {
  state: (): { toasts: Toast[] } => ({
    toasts: [],
  }),
  actions: {
    add(toast: Toast) {
      this.toasts.push(toast);

      setTimeout(() => {
        this.toasts = this.toasts.filter(t => t.id !== toast.id);
      }, defaultTimeout);
    },

    remove(id: string) {
      this.toasts = this.toasts.filter(t => t.id !== id);
    },
  },
});
