// Array holding the different phrase options
var phrases = [
  ["The turkey", "Dad", "The dog", "My teacher", "The elephant", "The cat", "My friend", "The chicken"],
  ["sat on", "ate", "danced with", "saw", "doesn't like", "kissed", "ran to", "jumped on"],
  ["a funny", "a scary", "a slimy", "a fat", "a goofy", "a barking", "a hairy", "a tiny"],
  ["goat", "monkey", "frog", "worm", "bug", "elephant", "cat", "lion"],
  ["on the moon", "in my soup", "on the chair", "in my spaghetti", "on the grass", "in the garage", "under the bed", "on the roof"]
];

// Array stores the user's current selections
var selected = ["", "", "", "", ""];

// Runs when a button is clicked updates the selected phrase
function select(column, index) {
  selected[column] = phrases[column][index];
  updateStory(); // Update the story text after selection
}

// Picks a random phrase from each column to make a sentence
function surprise() {
  for (var i = 0; i < phrases.length; i++) {
    var rand = Math.floor(Math.random() * phrases[i].length);
    selected[i] = phrases[i][rand];
  }
  updateStory(); // Refresh the story output
}

// Clears the current sentence and resets the output
function clearStory() {
  selected = ["", "", "", "", ""]; // Reset selected array
  document.getElementById("storyOutput").innerText = ""; // Clear the display
}

// Joins all selected words into one sentence
function updateStory() {
  var story = selected.join(" "); // Make a sentence from selected phrases
  document.getElementById("storyOutput").innerText = story;
}
