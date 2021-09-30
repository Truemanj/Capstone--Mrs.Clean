//toggle
const menutoggle = document.querySelector('.toggle');
const navigation = document.querySelector('.navigation');
menutoggle.onclick = () => {
    menutoggle.classList.toggle('active')
    navigation.classList.toggle('active')
}