import { useToasterStore } from '@/stores/toaster';

export function useToast() {
  const store = useToasterStore();

  const show = (
    status: 'success' | 'error' | 'warning',
    title: string,
  ) => {
    store.add({
      id: Date.now().toString(),
      status,
      title,
    });
  };

  return {
    success: (title: string) =>
      show('success', title),
    error: (title: string) =>
      show('error', title),
    warning: (title: string) =>
      show('warning', title),
  };
}
