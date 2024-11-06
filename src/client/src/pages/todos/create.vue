<script setup lang="ts">
import {
  type TodoClientModel,
  type CreateTodoRequest,
  TodosClient,
} from '../../api/api-client';
import { ref } from 'vue';
import axios from 'axios';
import { useRouter } from 'vue-router';

const loading = ref(true);
const data = ref(<TodoClientModel[]>[]);
const router = useRouter();
const selectedDate = ref('2024/11/06');

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
  body.value.date = new Date(selectedDate.value);
  await client
    .createTodoRequest(body.value, undefined)
    .then(async (response) => {
      const responseData = await response?.data.text();
      const dataModel = JSON.parse(responseData!) as TodoClientModel[];
      data.value = dataModel;
    })
    .finally(() => {
      loading.value = false;
      router.push('/todos');
    });
}
</script>

<template>
  <q-page>
    <div class="row flex-center q-ma-md">
      <q-input filled v-model="body.todoBody" label="Todo body" />
    </div>
    <div class="row flex-center q-ma-md">
      <q-card>
        <q-card-section>
          <div class="text-h6">Edit Expiration date</div>
          {{ selectedDate }}
        </q-card-section>
        <q-card-section class="q-pt-none">
          <div class="q-gutter-md row items-start">
            <q-date v-model="selectedDate" title="Select Expiration Date" />
          </div>
        </q-card-section>
      </q-card>
    </div>
    <div class="row flex-center q-ma-md">
      <q-btn @click="createNewTodoHandler()"> Create </q-btn>
      <RouterLink to="/todos">
        <q-btn class="q-ml-md"> Back </q-btn>
      </RouterLink>
    </div>
  </q-page>
</template>
