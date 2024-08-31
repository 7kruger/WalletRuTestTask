<script setup>
import * as signalR from '@microsoft/signalr'
import {ref} from 'vue'

let connection = new signalR.HubConnectionBuilder()
  .withUrl(`${import.meta.env.VITE_API_URL}/messages`, { withCredentials: false })
  .build();

connection.on("Receive", message => {
  messages.value.push(message)
});

connection.start()

const messages = ref([])
</script>

<template>
  <div class="d-flex justify-start align-center flex-column">
    <div>
      <h1>Real-time messages</h1>
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
