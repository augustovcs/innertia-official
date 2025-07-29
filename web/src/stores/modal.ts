import { defineStore } from 'pinia';

interface Modal {
  isOpen: boolean;
}

type ModalsMap = Record<string, Modal>;

export const useModalStore = defineStore('modal', {
  state: (): { modals: ModalsMap } => ({
    modals: {},
  }),
  actions: {
    create(name: string) {
      if (!this.modals[name]) {
        this.modals[name] = { isOpen: false };
      }
    },
    open(name: string) {
      if (this.modals[name]) {
        this.modals[name].isOpen = true;
      }
    },
    close(name: string) {
      if (this.modals[name]) {
        this.modals[name].isOpen = false;
      }
    },
    toggle(name: string) {
      if (this.modals[name]) {
        this.modals[name].isOpen = !this.modals[name].isOpen;
      }
    },
    delete(name: string) {
      if (this.modals[name]) {
        delete this.modals[name];
      }
    },
  },
});
