<template>
    <div class="lastMatches">
        <h1>Last 10 Matches</h1>
        <ul v-if="matches.length">
            <li v-for="match in reversedMatches" :key="match.id">
                {{ match.player1 }} vs {{ match.player2 }} |
                Score: {{ match.score1 }} - {{ match.score2 }}
            </li>
        </ul>
        <p v-else>No matches available.</p>
    </div>
</template>

<script>
export default {
    props: {
        matches: {
            type: Array,
            required: true,
        },
    },
    computed: {
        reversedMatches() {
            return this.matches.slice().reverse();
        },
    },
};
</script>

<style scoped>
.lastMatches {
    padding-left: 100px;
    padding-top: 20px;
}
@media (max-width: 1000px) {
  .lastMatches {
    padding-left: 0;
  }
}
</style>