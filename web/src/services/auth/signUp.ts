import { api } from '@/utils/api';
import { z } from 'zod';

export const signUpSchema = z
  .object({
    fullname: z
      .string('Insira o seu nome')
      .refine(
        val => val.trim().split(' ').length >= 2,
        'Insira o nome completo',
      ),
    email: z.email('Insira um email válido'),
    password: z
      .string('Campo obrigatório')
      .min(6, 'A senha deve conter pelo menos 6 caracteres'),
    confirmPassword: z.string('As senhas não coincidem'),
    termsAccepted: z.boolean(),
  })
  .superRefine(({ password, confirmPassword, termsAccepted }, ctx) => {
    if (password !== confirmPassword) {
      ctx.addIssue({
        code: 'custom',
        message: 'As senhas não coincidem',
        path: ['confirmPassword'],
      });
    }
    if (!termsAccepted) {
      ctx.addIssue({
        code: 'custom',
        message: 'A concordância com os termos é obrigatória',
        path: ['termsAccepted'],
      });
    }
  });

export type TSignUpSchema = z.infer<typeof signUpSchema>;

interface ISignUpRequest {
  email: string;
  password_hash: string;
}

interface ISignUpResponse {
  message: string;
  email: string;
}

export const signUp = (req: ISignUpRequest) => {
  const data = api.post<ISignUpResponse>('auth/register', req);

  return data;
};
