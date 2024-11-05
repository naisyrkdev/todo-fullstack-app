import { RouteRecordRaw } from 'vue-router';

const routes: RouteRecordRaw[] = [
  {
    path: '/',
    component: () => import('layouts/MainLayout.vue'),
    children: [
      {
        path: '',
        component: () => import('pages/IndexPage.vue'),
      },
      {
        path: '/todos',
        component: () => import('pages/todos/index.vue'),
      },
      {
        path: '/todos/create',
        component: () => import('pages/todos/create.vue'),
      },
      {
        path: '/todo/edit/:id',
        name: 'edit-todo',
        component: () => import('pages/todos/edit[id].vue'),
        props: true,
      },
    ],
  },

  // Always leave this as last one,
  // but you can also remove it
  {
    path: '/:catchAll(.*)*',
    component: () => import('pages/ErrorNotFound.vue'),
  },
];

export default routes;
