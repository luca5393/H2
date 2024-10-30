<template>
    <div>
        <h1>New Match</h1>
        <form @submit.prevent="submitMatch">
            <div class="user-container">
                <div>
                    <label for="player1">User 1 Name:</label>
                    <input type="text" id="player1" v-model="player1Name" required />
                </div>
                <div>
                    <label for="score1">User 1 Score:</label>
                    <input type="number" id="score1" v-model.number="player1Score" required />
                </div>
            </div>
            <div class="user-container">
                <div>
                    <label for="player2">User 2 Name:</label>
                    <input type="text" id="player2" v-model="player2Name" required />
                </div>
                <div>
                    <label for="score2">User 2 Score:</label>
                    <input type="number" id="score2" v-model.number="player2Score" required />
                </div>
            </div>
            <button type="submit">Submit Match</button>
        </form>
    </div>
</template>

<script>
export default {
    data() {
        return {
            player1Name: '',
            player2Name: '',
            player1Score: null,
            player2Score: null,
        };
    },
    methods: {
        async submitMatch() {
            const matchData = {
                player1: this.player1Name,
                player2: this.player2Name,
                score1: this.player1Score,
                score2: this.player2Score,
            };
            try {
                const response = await fetch('http://10.130.54.47:3001/api/addmatch', {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json',
                    },
                    body: JSON.stringify(matchData),
                });

                if (!response.ok) {
                    throw new Error('Network response was not ok');
                }
                this.player1Name = '';
                this.player2Name = '';
                this.player1Score = null;
                this.player2Score = null;

                this.$emit('match-submitted');

            } catch (error) {
                console.error('Error submitting match:', error);
            }
        }
    }
};
</script>

<style scoped>

form {
    display: flex;
    flex-direction: column;
}
.user-container {
    padding: 5px;
}
label {
    margin: 10px 0 5px;
}

input {
    margin-bottom: 15px;
    padding: 8px;
    margin: 0 5px 10px 10px;
}
</style>