import { useToasterStore } from '@/stores/toaster';

export function useToast() {
  const store = useToasterStore();

  const show = (
    status: 'success' | 'error' | 'warning',
    title: string,
    description = '',
  ) => {
    store.add({
      id: Date.now().toString(),
      status,
      title,
      description,
    });
  };

  return {
    success: (title: string, description = '') =>
      show('success', title, description),
    error: (title: string, description = '') =>
      show('error', title, description),
    warning: (title: string, description = '') =>
      show('warning', title, description),
  };
}
