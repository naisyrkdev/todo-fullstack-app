<script setup lang="ts">
import { type Todo, TodosClient } from '../../api/api-client';
import { ref, onBeforeMount } from 'vue';
import axios from 'axios';

const loading = ref(true);
const data = ref(<Todo[]>[]);

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
      <div v-if="loading">
        <q-spinner color="primary" size="5em" :thickness="15" />
      </div>
      <div v-else>
        <q-list>
          <q-item> </q-item>
        </q-list>
      </div>
    </div>
  </q-page>
</template>
