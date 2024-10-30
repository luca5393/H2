const express = require('express');
const cors = require('cors');
const fs = require('fs');
const path = require('path');
const app = express();
const PORT = 3001;

const dataPath = path.join(__dirname, 'data.json');
let data;

fs.readFile(dataPath, 'utf8', (err, jsonData) => {
  if (err) {
    console.error('Error reading JSON file:', err);
    return;
  }
  data = JSON.parse(jsonData);
});

app.use(cors({
    origin: 'http://10.130.54.47:3000',
    methods: ['GET', 'POST'],
}));

app.use(express.json());

app.get('/api/users', (req, res) => {
  res.json(data.users);
});

app.get('/api/matches', (req, res) => {
    const lastTenMatches = data.matches.slice(-10);
    res.json(lastTenMatches);
  });
  

app.post('/api/addmatch', (req, res) => {
    const newMatch = req.body;

    if (!newMatch.player1 || !newMatch.player2 || newMatch.score1 == null || newMatch.score2 == null || newMatch.player1 == newMatch.player2) {
      console.error('Invalid match data:', newMatch);
      return res.status(400).json({ message: 'Invalid match data' });
    }
  
    const { score1, score2 } = newMatch;

    data.matches.push(newMatch);    
    addGame(newMatch.player1, score1 > score2);
    addGame(newMatch.player2, score2 > score1);
    
    fs.writeFile(dataPath, JSON.stringify(data, null, 2), (err) => {
      if (err) {
        console.error('Error writing JSON file:', err);
        return res.status(500).json({ message: 'Internal server error' });
      }
      res.status(201).json(newMatch);
    });
});
  
function addGame(userName, win) {
    const user = data.users.find(user => user.name === userName);
    if (!user) {
        data.users.push({
            "name": userName,
            "wins": win ? 1 : 0,
            "totalGames": 1
        });
    } else {
        user.wins += win ? 1 : 0;
        user.totalGames += 1;
    }
}

app.listen(PORT, '0.0.0.0', () => {
    console.log(`Server running at http://0.0.0.0:${PORT}`);
  });
