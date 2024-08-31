<script setup>
import {ref} from 'vue'
import '@/utils/dateExtension'

const messages = ref([])
const startDate = ref(new Date(Date.now() - 1000 * 600))
const endDate = ref()

async function loadRecentMessagesAsync() {
  const url = new URL(`${import.meta.env.VITE_API_URL}/api/messages`);
  const params = new URLSearchParams();

  if (startDate.value) {
    params.append('startDateTime', startDate.value.toISOString());
  }
  if (endDate.value) {
    params.append('endDateTime', endDate.value.toISOString());
  }

  url.search = params.toString();

  const response = await fetch(url)

  if (response.ok) {
    const result = await response.json()
    messages.value = result.data
  }
}

await loadRecentMessagesAsync()
</script>

<template>
  <div class="d-flex justify-start align-center flex-column">
    <div class="mb-2">
      <h1 class="text-center">Recent messages</h1>
    </div>
    <div class="mb-3 d-flex justify-center">
      <div class="ms-2 me-2">
        Start:
        <VueDatePicker dark v-model="startDate" />
      </div>
      <div class="ms-2 me-2">
        End:
        <VueDatePicker dark v-model="endDate" />
      </div>
      <div class="d-flex align-end ms-2">
        <v-btn @click="loadRecentMessagesAsync">
          Apply
        </v-btn>
      </div>
    </div>
    <div style="width: 900px; max-width: 900px">
      <v-list
        v-if="messages.length !== 0"
        lines="three"
      >
        <v-list-item
          v-for="message in messages"
          :value="message"
          class="ps-5 pe-5"
        >
          <v-list-item-title>
            {{ message.body }}
          </v-list-item-title>

          <v-list-item-subtitle>
            <b>serial number:</b> {{ message.serialNumber }}<br>
            <b>timestamp:</b> {{ message.timestamp }}
          </v-list-item-subtitle>
        </v-list-item>
      </v-list>
      <div v-else>
        <h3 class="text-center">No messages</h3>
      </div>
    </div>
  </div>
</template>
