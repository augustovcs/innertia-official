import { onMounted, ref, watch } from 'vue';

export type Theme = 'light' | 'dark';

const theme = ref<Theme>('light');

export function useTheme() {
  const toggleTheme = () => {
    theme.value = theme.value === 'dark' ? 'light' : 'dark';
  };

  const updateDOM = () => {
    document.documentElement.classList.remove('dark', 'light');
    document.documentElement.classList.add(theme.value);
    localStorage.setItem('theme', theme.value);
  };

  onMounted(() => {
    const saved = localStorage.getItem('theme') as Theme | null;
    theme.value =
      saved ??
      (window.matchMedia('(prefers-color-scheme: dark)').matches
        ? 'dark'
        : 'light');
    updateDOM();
  });

  watch(theme, updateDOM);

  return {
    theme,
    toggleTheme,
  };
}
