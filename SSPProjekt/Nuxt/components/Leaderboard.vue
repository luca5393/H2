<template>
  <div class="leaderboard">
    <h1>Leaderboard</h1>
    <ul v-if="users.length">
      <li v-for="user in users" :key="users.id">
        {{ user.name }} Wins:{{ user.wins }} Games:{{ user.totalGames }} Win%:{{ getWinProcent(user.wins,
          user.totalGames) }}
      </li>
    </ul>
    <p v-else>No matches available.</p>
  </div>
</template>

<script>
export default {
  props: {
    users: {
      type: Array,
      required: true,
    },
  },
  methods: {
    getWinProcent(wins, games) {
      return games > 0 ? Math.round((wins / games) * 100) : 0;
    },
  },
}
</script>

<style scoped>
.leaderboard {
  padding-right: 100px;
  padding-top: 20px;
}
@media (max-width: 1000px) {
  .leaderboard {
    padding-right: 0;
  }
}
</style>