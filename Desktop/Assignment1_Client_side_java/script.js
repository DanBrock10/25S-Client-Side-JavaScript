var phrases = [
  ["The turkey", "Dad", "The dog", "My teacher", "The elephant", "The cat", "My friend", "The chicken"],
  ["sat on", "ate", "danced with", "saw", "doesn't like", "kissed", "ran to", "jumped on"],
  ["a funny", "a scary", "a slimy", "a fat", "a goofy", "a barking", "a hairy", "a tiny"],
  ["goat", "monkey", "frog", "worm", "bug", "elephant", "cat", "lion"],
  ["on the moon", "in my soup", "on the chair", "in my spaghetti", "on the grass", "in the garage", "under the bed", "on the roof"]
];

var selected = ["", "", "", "", ""];

function select(column, index) {
  selected[column] = phrases[column][index];
  updateStory();
}
