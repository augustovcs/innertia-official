<script setup lang="ts">
import LogoIcon from '@/components/LogoIcon.vue';
import { useDashboardStore, type DashboardPages } from '@/stores/dashboard';
import {
	Calendar,
	Home,
	List,
	LogOut,
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
</script>

<template>
  <div class="min-h-full">
    <aside
      :class="`flex flex-col justify-between transition-all duration-300 ease-in-out overflow-hidden min-w-[16rem] min-h-full border-r-2 border-border/60`"
    >
      <div class="h-full flex flex-col gap-6">
        <div class="ml-8 mt-4">
          <LogoIcon height="64" width="150" class="text-fg" />
        </div>
        <ul
          :class="`flex flex-col justify-items-center 2xl:justify-items-baseline gap-y-4 mx-2 py-4 transition-all duration-300 mb-4`"
        >
          <li
            v-for="{ name, icon, path } in generalMenu"
            :key="name"
            @click="() => store.current(path)"
            :class="`cursor-pointer transition-all duration-300 ease-in-out flex gap-x-4 p-[0.65rem] rounded-2xl
						  ${store.page === path ? 'text-primary bg-primary/15' : 'text-primary/70 hover:text-primary hover:bg-primary/5'}`"
          >
            <component
              :is="icon"
              :class="`transition-all duration-300 ease-in-out overflow-hidden whitespace-nowrap ml-2 mr-1 size-6`"
            />
            <h4
              :class="`transition-all duration-300 ease-in-out overflow-hidden whitespace-nowrap 
              opacity-100 w-[10rem] font-ibm-plex-sans font-medium`"
            >
              {{ name }}
            </h4>
          </li>
        </ul>
      </div>
      <div class="flex flex-col gap-y-2 border-t-2 border-zinc-300/20 pt-4 m-2 mb-8 pl-1">
        <button
          class="cursor-pointer text-primary/80 hover:text-primary selected:bg-red-300/70 hover:bg-primary/10 transition-all duration-250 flex gap-x-4 rounded-2xl p-[0.65rem]"
					@click="store.current('/settings')"
        >
          <Settings
            :class="`transition-all duration-300 ease-in-out overflow-hidden whitespace-nowrap ml-2 size-6`"
          />
          <h4
            :class="`transition-all duration-300 ease-in-out overflow-hidden whitespace-nowrap opacity-100 font-medium`"
          >
            Configurações
          </h4>
        </button>
        <button
          class="cursor-pointer text-primary/80 hover:text-error/80 selected:bg-red-300/70 hover:bg-error/15 transition-all duration-250 flex gap-x-4 rounded-2xl p-[0.65rem]"
        >
          <LogOut
            :class="`transition-all duration-300 ease-in-out overflow-hidden whitespace-nowrap ml-2 size-6`"
          />
          <h4
            :class="`transition-all duration-300 ease-in-out overflow-hidden whitespace-nowrap opacity-100 font-medium`"
          >
            Sair
          </h4>
        </button>
      </div>
    </aside>
  </div>
</template>
