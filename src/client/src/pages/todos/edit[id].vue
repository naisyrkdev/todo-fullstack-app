<script setup lang="ts">
import {
  type Todo,
  type CreateTodoRequest,
  TodosClient,
} from '../../api/api-client';
import { ref } from 'vue';
import axios from 'axios';

const loading = ref(true);
const data = ref(<Todo[]>[]);

const body = ref(<CreateTodoRequest>{
  date: new Date(),
  todoBody: '',
});

async function createNewTodoHandler() {
  loading.value = true;
  const axiosInstance = axios;
  const client = new TodosClient(
    import.meta.env.VITE_API_BASE_PATH,
    axiosInstance
  );
  await client
    .createTodoRequest(body.value, undefined)
    .then(async (response) => {
      const responseData = await response?.data.text();
      const dataModel = JSON.parse(responseData!) as Todo[];
      if (import.meta.env.VITE_MODE == 'DEVELOPMENT')
        console.log('Fetched data: ', dataModel);
      data.value = dataModel;
    })
    .finally(() => (loading.value = false));
}
</script>

<template>
  <q-page>
    <div class="row flex-center">
      <q-input filled v-model="body.todoBody" label="Todo body" />
    </div>
    <q-btn @click="createNewTodoHandler()"> Edit </q-btn>
  </q-page>
</template>
