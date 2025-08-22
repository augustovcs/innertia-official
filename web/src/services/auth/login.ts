import { api } from '@/utils/api';
import { z } from 'zod';

export const loginSchema = z.object({
  email: z.email('Insira um email válido'),
  password: z
    .string('Campo obrigatório')
    .min(6, 'A senha deve conter pelo menos 6 caracteres'),
});

export type TLoginSchema = z.infer<typeof loginSchema>;

interface ILoginRequest {
  email: string;
  password: string;
}

interface ILoginResponse {
  message: string;
  email: string;
}

export const login = (req: ILoginRequest) => {
  const data = api.post<ILoginResponse>('auth/login', req);

  return data;
};
