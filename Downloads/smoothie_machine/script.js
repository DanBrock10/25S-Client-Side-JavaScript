class Smoothie {
  constructor(name, size, base, ingredients, notes) {
    this.name = name;
    this.size = size;
    this.base = base;
    this.ingredients = ingredients;
    this.notes = notes;
  }

  calculatePrice() {
    return 3.0 + this.ingredients.length * 0.5;
  }

  describe() {
    const price = this.calculatePrice().toFixed(2);
    return `
      <h3>${this.name}'s ${this.size} ${this.base} Smoothie 🧋</h3>
      <ul>
        ${this.ingredients.map(i => `<li>${i}</li>`).join('')}
      </ul>
      <p>Notes: ${this.notes || 'None'}</p>
      <p>Price: <strong>$${price}</strong></p>
      <hr/>
    `;
  }
}

const order = [];

document.getElementById('smoothieForm').addEventListener('submit', function(e) {
  e.preventDefault();

  const name = document.getElementById('customerName').value;
  const size = document.getElementById('size').value;
  const base = document.getElementById('base').value;

  const ingredients = [];
  document.querySelectorAll('input[type="checkbox"]:checked').forEach(cb => {
    ingredients.push(cb.value);
  });

  if (ingredients.length === 0) {
    alert("Please select at least one ingredient.");
    return;
  }

  const notes = document.getElementById('notes').value;

  const smoothie = new Smoothie(name, size, base, ingredients, notes);
  order.push(smoothie);

  renderOrder();

  const sizeImagesContainer = document.getElementById('sizeImages');
  const sizeImg = document.createElement('img');
  const sizeImages = {
    "Small": "images/small.png",
    "Medium": "images/medium.png",
    "Large": "images/large.png"
  };
  sizeImg.src = sizeImages[size];
  sizeImg.alt = `${size} Drink`;
  sizeImagesContainer.appendChild(sizeImg);

  document.getElementById('smoothieForm').reset();
});

function renderOrder() {
  const output = document.getElementById('output');
  output.innerHTML = `<h2>Your Order</h2>`;

  let total = 0;
  order.forEach(smoothie => {
    output.innerHTML += smoothie.describe();
    total += smoothie.calculatePrice();
  });

  output.innerHTML += `<h3>Total: $${total.toFixed(2)}</h3>`;
}
