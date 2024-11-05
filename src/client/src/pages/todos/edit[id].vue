<script setup lang="ts">
import {
  type Todo,
  type CreateTodoRequest,
  TodosClient,
  EditTodoRequest,
} from '../../api/api-client';
import { onBeforeMount, ref } from 'vue';
import axios from 'axios';

import { useRouter } from 'vue-router';
const router = useRouter();
const loading = ref(true);
const data = ref(<Todo>{});

const props = defineProps<{
  id: string;
}>();

onBeforeMount(async () => {
  await assignDataToClientModel();
});

async function assignDataToClientModel() {
  loading.value = true;
  const axiosInstance = axios;
  const client = new TodosClient(
    import.meta.env.VITE_API_BASE_PATH,
    axiosInstance
  );
  await client
    .getTodoByIdRequest(props.id, undefined)
    .then(async (response) => {
      const responseData = await response?.data.text();
      const dataModel = JSON.parse(responseData!) as Todo;
      if (import.meta.env.VITE_MODE == 'DEVELOPMENT')
        console.log('Fetched data: ', dataModel);
      data.value = dataModel;
    })
    .finally(() => (loading.value = false));
}

async function editTodoHandler() {
  loading.value = true;
  const axiosInstance = axios;
  const client = new TodosClient(
    import.meta.env.VITE_API_BASE_PATH,
    axiosInstance
  );
  const body = <EditTodoRequest>{
    id: props.id,
    date: new Date(),
    todoBody: data.value.todoBody,
  };
  await client
    .editTodoRequest(body, undefined)
    .then(async (response) => {
      const responseData = await response?.data.text();
    })
    .finally(() => {
      loading.value = false;
      router.push('/todos');
    });
}
</script>

<template>
  <q-page>
    <div class="row flex-center">
      <div class="row flex-center">
        <q-input filled v-model="data.todoBody" label="Todo body" />
      </div>
    </div>
    <div class="row flex-center q-ma-md">
      <q-btn @click="editTodoHandler()"> Edit </q-btn>
      <RouterLink to="/todos">
        <q-btn class="q-ml-md"> Back </q-btn>
      </RouterLink>
    </div>
  </q-page>
</template>
