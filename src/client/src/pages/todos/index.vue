<script setup lang="ts">
import {
  type TodoClientModel,
  type ChangeTodoStateRequest,
  TodosClient,
  EditTodoRequest,
} from '../../api/api-client';
import { ref, onBeforeMount } from 'vue';
import axios from 'axios';

const loading = ref(true);
const data = ref(<TodoClientModel[]>[]);
const alert = ref(false);
const selectedDate = ref('2019/02/01');
const selectedId = ref('');

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
    .getTodosRequest(undefined)
    .then(async (response) => {
      const responseData = await response?.data.text();
      const dataModel = JSON.parse(responseData!) as TodoClientModel[];
      if (import.meta.env.VITE_MODE == 'DEVELOPMENT')
        console.log('Fetched data: ', dataModel);
      data.value = dataModel;
    })
    .finally(() => (loading.value = false));
}

async function changeStateHandler(id: string) {
  loading.value = true;
  const axiosInstance = axios;
  const client = new TodosClient(
    import.meta.env.VITE_API_BASE_PATH,
    axiosInstance
  );
  const body: ChangeTodoStateRequest = {
    id: id,
  };
  await client
    .chagneStateRequest(body, undefined)
    .then(async (response) => {
      const responseData = await response?.data.text();
      const dataModel = JSON.parse(responseData!) as TodoClientModel[];
      if (import.meta.env.VITE_MODE == 'DEVELOPMENT')
        console.log('Fetched data: ', dataModel);
      data.value = dataModel;
    })
    .finally(() => (loading.value = false));
}

async function showDatePopup(id: string) {
  alert.value = true;
  selectedId.value = id;
}

async function changeDate(id: string) {
  alert.value = true;
  loading.value = true;
  const axiosInstance = axios;
  const client = new TodosClient(
    import.meta.env.VITE_API_BASE_PATH,
    axiosInstance
  );
  const body: EditTodoRequest = {
    id: id,
    date: new Date(selectedDate.value),
    todoBody: '',
  };
  console.log('body here', body);
  await client
    .editTodoRequest(body, undefined)
    .then(async () => {
      await assignDataToClientModel();
    })
    .finally(() => {
      loading.value = false;
      alert.value = false;
      selectedDate.value = '2019/02/01';
      selectedId.value = '';
    });
}

async function removeTodo(id: string) {
  loading.value = true;
  const axiosInstance = axios;
  const client = new TodosClient(
    import.meta.env.VITE_API_BASE_PATH,
    axiosInstance
  );
  await client
    .removeTodoRequest(id, undefined)
    .then(async () => {
      await assignDataToClientModel();
    })
    .finally(() => {
      loading.value = false;
    });
}
</script>

<template>
  <q-page>
    <q-dialog v-model="alert">
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

        <q-card-actions align="right">
          <q-btn flat label="Close" color="primary" v-close-popup />
          <q-btn
            flat
            label="Save"
            color="primary"
            @click="changeDate(selectedId)"
          />
        </q-card-actions>
      </q-card>
    </q-dialog>
    <div class="row flex-center">
      <div v-if="loading">
        <q-spinner color="primary" size="5em" :thickness="15" />
      </div>
      <div v-else>
        <q-list>
          <q-item v-for="todo in data" :key="todo.id">
            <div class="q-ma-md" :class="{ 'crossed-out-text': todo.isDone }">
              <a class="text-weight-bold q-mr-md">
                {{ todo.todoBody }}
              </a>
              has to be done before
              <a class="text-weight-bold q-ml-md"> {{ todo.expirenceDate }} </a>
            </div>
            <q-checkbox
              v-model="todo.isDone"
              @click="changeStateHandler(todo.id)"
            />
            <q-btn @click="showDatePopup(todo.id)"> Change Date </q-btn>
            <q-btn @click="removeTodo(todo.id)" class="q-ml-md"> Remove </q-btn>

            <RouterLink :to="{ name: 'edit-todo', params: { id: todo.id } }">
              <q-btn class="q-ml-md" size="xl"> Edit </q-btn>
            </RouterLink>
          </q-item>
        </q-list>
      </div>
    </div>
    <div class="row flex-center">
      <RouterLink to="/todos/create">
        <q-btn class="q-ml-md"> Create </q-btn>
      </RouterLink>
    </div>
  </q-page>
</template>

<style scoped>
.crossed-out-text {
  text-decoration: line-through !important;
}
</style>
