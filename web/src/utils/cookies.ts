import { inject } from 'vue';
import type { VueCookies } from 'vue-cookies';

let $cookies: VueCookies | undefined

export const useCookies = () => $cookies = inject<VueCookies>('$cookies');

$cookies?.config('7d', '/', '', true, 'Lax');

export const setCookie = (key: string, value: any) => {
  console.log($cookies)
  $cookies?.set(key, value);
};

export const getCookie = (key: string) => {
  return $cookies?.get(key);
};

export const removeCookie = (key: string) => {
  return $cookies?.remove(key);
};
