<template>
  <div class="container">
    <LastMatches :matches="matches"/>
    <div class="main-center-content">
      <h1>Sten, saks, papir Site</h1>
      <button @click="reload">Reload</button>
      <NewMatch @match-submitted="reload" />
    </div>
    <Leaderboard :users="users"/>

    
  </div>
</template>

<script>
export default {
  data() {
    return {
      matches: [],
      users: [],
    };
  },
  async mounted() {
    await this.fetchMatches();
    await this.fetchUsers();
  },
  methods: {
    async fetchMatches() {
      try {
        const response = await fetch('http://10.130.54.47:3001/api/matches');
        this.matches = await response.json();
      } catch (error) {
        console.log('Error fetching matches:', error);
      }
    },
    async fetchUsers() {
      try {
        const response = await fetch('http://10.130.54.47:3001/api/users');
        this.users = await response.json();
        this.orderMatches();
      } catch (error) {
        console.log('Error fetching matches:', error);
      }
    },
    orderMatches() {
      this.users.sort((a, b) => {
        const winRateA = this.getWinProcent(a.wins, a.totalGames);
        const winRateB = this.getWinProcent(b.wins, b.totalGames);
        return winRateB - winRateA;
      });
    },
    reload() {
      this.fetchMatches();
      this.fetchUsers();
    },
    getWinProcent(wins, games) {
      return games > 0 ? Math.round((wins / games) * 100) : 0;
    },
  },
};
</script>

<style>
.container {
  display: flex;
  justify-content: space-between;

  .main-center-content {
    text-align: center;
  }
}

ul {
    list-style-type: none;
    padding: 0;
}

li {
    background: #f4f4f4;
    margin: 5px 0;
    padding: 10px;
    border-radius: 5px;
}

h1 {
  color: #333;
}

button {
  background-color: #f4f4f4;
  color: black;
  padding: 10px 20px;
  border-radius: 5px;
  border-color: gray;
  font-size: 16px;
  cursor: pointer;
  transition: background-color 0.3s, transform 0.2s;
}

button:hover {
  background-color: #f4f4f46b;
  transform: scale(1.05);
}

button:active {
  transform: scale(0.95);
}

</style>
