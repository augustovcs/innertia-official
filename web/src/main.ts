import '@/styles/main.css';

import App from '@/App.vue';
import router from '@/routes';
import { createPinia } from 'pinia';
import { createApp } from 'vue';
import VueCookies from 'vue-cookies';

import Dashboard from './layouts/Dashboard.vue';
import Default from './layouts/Default.vue';

const pinia = createPinia();

createApp(App)
  .component('default-layout', Default)
  .component('dashboard-layout', Dashboard)
  .use(router)
  .use(pinia)
  .use(VueCookies)
  .mount('#app');
