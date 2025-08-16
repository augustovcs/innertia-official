import { getCookie, useCookies } from '@/utils/cookies';
import type { NavigationGuardNext } from 'vue-router';

export async function isAuthenticated(
  expected: 'authenticated' | 'not-authenticated',
  next: NavigationGuardNext,
) {
  useCookies();
  const token = getCookie('session_token');

  if (!token) {
    if (expected === 'not-authenticated') next();
    else next('/login');
  } else {
		if (expected === 'authenticated') next();
		else next('/dashboard');
	};
}
