<script setup lang="ts">
import LogoIcon from '@/components/LogoIcon.vue';
import { useDashboardStore, type DashboardPages } from '@/stores/dashboard';
import {
	Calendar,
	Home,
	List,
	MessageCircleQuestion,
	NotebookPen,
	Settings,
	type LucideProps,
} from 'lucide-vue-next';
import type { FunctionalComponent } from 'vue';

const store = useDashboardStore();

type MenuItems = {
  name: string;
  icon: FunctionalComponent<LucideProps, {}, any, {}>;
  path: DashboardPages;
};

const generalMenu: MenuItems[] = [
  { name: 'Visão Geral', icon: Home, path: '/' },
  { name: 'Caledário', icon: Calendar, path: '/calendar' },
  { name: 'Tarefas', icon: List, path: '/tasks' },
  { name: 'Agenda', icon: NotebookPen, path: '/notes' },
];

const othersMenu: MenuItems[] = [
  { name: 'Configurações', icon: Settings, path: '/settings' },
  {
    name: 'Centro de Ajuda',
    icon: MessageCircleQuestion,
    path: '/help-center',
  },
];
</script>

<template>
  <div class="h-screen max-h-screen pr-4 border-r-2 border-border/50">
    <aside
      class="flex flex-col justify-between transition-all duration-300 ease-in-out overflow-hidden min-w-[16rem] min-h-full pl-4.5"
    >
      <div class="h-full flex flex-col gap-10">
        <div class="mt-4">
          <LogoIcon height="64" width="154" class="text-fg" />
        </div>
        <ul
          class="flex flex-col justify-items-center 2xl:justify-items-baseline gap-y-4 transition-all duration-300 mb-4"
        >
          <li
            v-for="{ name, icon, path } in generalMenu"
            :key="name"
            @click="() => store.current(path)"
            :class="`cursor-pointer transition-all duration-300 ease-in-out flex items-center gap-x-3 p-[0.65rem] py-2.5 rounded-lg
						  ${store.page === path ? 'text-fg bg-muted-fg/10' : 'text-muted-fg hover:text-primary'}`"
          >
            <component
              :is="icon"
              class="transition-all duration-300 ease-in-out overflow-hidden whitespace-nowrap ml-2 mr-1 size-5"
            />
            <h4
              class="transition-all duration-300 ease-in-out overflow-hidden whitespace-nowrap opacity-100 w-[10rem] font-ibm-plex-sans font-medium"
            >
              {{ name }}
            </h4>
          </li>
        </ul>
      </div>
      <ul
        class="flex flex-col justify-items-center 2xl:justify-items-baseline gap-y-2 mb-6 transition-all duration-300 border-t-2 border-border/50 pt-4"
      >
        <li
          v-for="{ name, icon, path } in othersMenu"
          :key="name"
          @click="() => store.current(path)"
          :class="`cursor-pointer transition-all duration-300 ease-in-out flex items-center gap-x-3 p-[0.65rem] py-2.5 rounded-lg
						  ${store.page === path ? 'text-fg bg-muted-fg/10' : 'text-muted-fg hover:text-primary'}`"
        >
          <component
            :is="icon"
            class="transition-all duration-300 ease-in-out overflow-hidden whitespace-nowrap ml-2 mr-1 size-5"
          />
          <h4
            class="transition-all duration-300 ease-in-out overflow-hidden whitespace-nowrap opacity-100 w-[10rem] font-ibm-plex-sans font-medium"
          >
            {{ name }}
          </h4>
        </li>
      </ul>
    </aside>
  </div>
</template>
