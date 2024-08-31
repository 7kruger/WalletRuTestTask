<template>
  <v-toolbar>
    <v-btn
      to="generate"
      class="ma-1"
    >
      <v-icon>mdi-forum-plus-outline</v-icon>

      <span>Generate messages (Client 1)</span>
    </v-btn>

    <v-btn
      to="realtime"
      class="ma-1"
    >
      <v-icon>mdi-list-box-outline</v-icon>

      <span>Realtime (Client 2)</span>
    </v-btn>

    <v-btn
      to="recent"
      class="ma-1"
    >
      <v-icon>mdi-history</v-icon>

      <span>Recent (Client 3)</span>
    </v-btn>
  </v-toolbar>
</template>
<script setup>
</script>
