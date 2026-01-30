# Interaction System - [Serhan Turgul]

> Ludu Arts Unity Developer Intern Case

## Proje Bilgileri

| Bilgi | Değer |
|-------|-------|
| Unity Versiyonu | 6000.3.0f1 |
| Render Pipeline |  URP |
| Case Süresi | 12 saat |
| Tamamlanma Oranı | %68.75 - Implement edilen özelliklerin yüzdesidir. |

---

## Kurulum

1. Repository'yi klonlayın:
```bash
git clone https://github.com/SerhanTRGL/Ludu-Arts-Case.git
```

2. Unity Hub'da projeyi açın
3. `Assets/Scenes/TestScene.unity` sahnesini açın
4. Play tuşuna basın

---

## Nasıl Test Edilir

### Kontroller

| Tuş | Aksiyon |
|-----|---------|
| WASD | Hareket |
| E | Etkileşim |


### Test Senaryoları

1. **Key Pickup Test**
   - Anahtarların olduğu bölgeye yaklaşın
   - Prompt çıktıkça interact butonuna basın
   - Her interact butonuna bastığınızda ektileşime geçtiğiniz anahtar haritadan silinecek ve envanterinize eklenecektir.

2. **Hold Interaction Test**
   - "Hold Interaction" yazılı bölgedeki kürelere doğru gidin.
   - Yeterince yaklaştığınızda promptu ve progress barı göreceksiniz.
   - Interact butonuna basılı tutun, kalan süreyi ve interaksiyonun sonunda sonucu göreceksiniz.

3. **Toggle Test**
   - "Toggle Interaction" yazılı bölgedeki kürelere doğru gidin.
   - Yeterince yaklaştığınızda kürelerin o anki durumlarına göre durumlarını değiştirmek için uygun promptu görecektsiniz.
   - Interact butonuna basarak kürelerin durumunu değiştirin.

---

## Mimari Kararlar

### Interaction System Yapısı

```
InteractionDetector: Periyodik olarak oyuncunun yakın çevresindeki interact edilebilecek bütün objelerin listesini tutar ve bu listeyi günceller.
InteractionController: Oyuncunun interact butonuna basması durumunda InteractionDetector'un bildirdiği en yakın InteractableObject ile etkileşimi başlatır. En yakın etkileşime girilebilecek obje için bir state machine görevi görür (start->tick->stop döngüsü). 

Her interaksiyon tipi için IInteractionDriver interface'ini implement eden bir class vardır (InstantInteractionDriver vb). Bu classlar her interaksiyon tipi için start->tick->stop akışının nasıl olacağını anlatır. 

IInteractable olan her objenin bir sürücüsü (IInteractableDriver) olmak zorundadır.

IInteractable interface'ini implement eden her Monobehavior interaksiyon başlangıcında, her güncellemede ve interaksiyon sonunda ne yapmak istediğini burada tanımlar. Burası oyundaki objenin doğrudan etkilendiği kısımdır.

```

**Neden bu yapıyı seçtim:**
> LLM'in yönlendirmesi sonucunda bu yapıyı seçtim.

**Alternatifler:**
> Başlangıçtaki fikrim her interaksiyon tipi için ayrı bir interface yazmaktı, ancak LLM'le yaptığım diyalogda bunun çok zayıf kalacağı ortaya çıktı. Eğer ayrı interface rotasından gitseydim muhtemelen aynı sonuca (state machine benzeri bir yapıya) ulaşacaktım, ancak kod çok daha karışık ve kırılgan olacaktı.

**Trade-off'lar:**
> Sistem kolaylıkla yeni interaksiyon tiplerinin eklenmesini destekliyor, ancak yeni eklencek her interaksiyon tipi için oldukça fazla boilerplate kod yazılması gerekiyor (Yeni bir InteractionDriver, ve UI kısmında yeni bir InteractionUIAdapter).
 
### Kullanılan Design Patterns

| Pattern | Kullanım Yeri | Neden |
|---------|---------------|-------|
| [Observer] | [Interactable objelerin durum değişikliklerinde] | UI elementlerine ve gerekli diğer componentlere değişiklik olduğunda haber verebilmek için |


---

## Ludu Arts Standartlarına Uyum

### C# Coding Conventions

| Kural | Uygulandı | Notlar |
|-------|-----------|--------|
| m_ prefix (private fields) | [x] / [ ] |Mümkün olduğunca uymaya çalıştım, sürenin sonlarına doğru özellikleri yetiştirmek için taviz verdiğim noktalar oldu|
| s_ prefix (private static) | [] / [x] | |
| k_ prefix (private const) | [] / [x] | |
| Region kullanımı | [] / [x] | |
| Region sırası doğru | [] / [x] | |
| XML documentation | [ ] / [x] | |
| Silent bypass yok | [] / [x] | |
| Explicit interface impl. | [] / [x] | |

### Naming Convention

| Kural | Uygulandı | Örnekler |
|-------|-----------|----------|
| P_ prefix (Prefab) | [x] / [ ] | P_Key, P_Larger_Key |
| M_ prefix (Material) | [x] / [ ] | M_Player_Head |
| T_ prefix (Texture) | Uygun değil, texture kullanmadım | |
| SO isimlendirme | [x] / [ ] | |

### Prefab Kuralları

| Kural | Uygulandı | Notlar |
|-------|-----------|--------|
| Transform (0,0,0) | [x] / [ ] | |
| Pivot bottom-center | [x] / [ ] | |
| Collider tercihi | [x] / [ ] | Box > Capsule > Mesh |
| Hierarchy yapısı | [] / [x] | |

### Zorlandığım Noktalar
> Şirket kalitesinde standartlara bireysel bir geliştirici olarak alışık olmadığım için oldukça zorlandım, ve çoğunlukla yerine getiremedim. 12 saatlik bir süre benim için hem istenilen özellikleri gerçekleştirmek hem de conventionların hepsini uygulamak için yeterli bir süre değildi. 
> Öncelikle özellikleri yetiştirip, sonrasında yazdığım kodu conventionlara göre düzenlemeyi düşündüm, ancak bu da bu zaman zarfında mümkün olmadı.

---

## Tamamlanan Özellikler

### Zorunlu (Must Have)

(Tamamlandı/Tamamlanmadı)
- [x] / [ ] Core Interaction System
  - [x] / [ ] IInteractable interface
  - [x] / [ ] InteractionDetector
  - [x] / [ ] Range kontrolü

- [x] / [ ] Interaction Types
  - [x] / [ ] Instant
  - [x] / [ ] Hold
  - [x] / [ ] Toggle

- [ ] / [x] Interactable Objects
  - [ ] / [x] Door (locked/unlocked)
  - [x] / [ ] Key Pickup
  - [ ] / [x] Switch/Lever
  - [ ] / [x] Chest/Container

- [x] / [ ] UI Feedback
  - [x] / [ ] Interaction prompt
  - [ ] / [x] Dynamic text
  - [x] / [ ] Hold progress bar
  - [ ] / [x] Cannot interact feedback

- [x] / [ ] Simple Inventory
  - [x] / [ ] Key toplama
  - [x] / [ ] UI listesi

### Bonus (Nice to Have)

- [ ] Animation entegrasyonu
- [ ] Sound effects
- [ ] Multiple keys / color-coded
- [ ] Interaction highlight
- [ ] Save/Load states
- [ ] Chained interactions

---

## Bilinen Limitasyonlar

### Tamamlanamayan Özellikler
1. [Özellik] - [Neden tamamlanamadı]
2. [Özellik] - [Neden]

### Bilinen Bug'lar
1. Instant Interaction objelerde eğer tespit edilen obje yakın çevredeki son objeyse UI promptu kapanmıyor. 
    1.1 Karakteri bir veya birden fazla instant interaction objesinin yanına götür.
    1.2 Son objeyle interakt edene kadar interakt tuşuna tekrar tekrar bas.
    1.3 Son objeyle interakt edildiğinde "Press to interact" promptu ortadan kalkmıyor.
### İyileştirme Önerileri
1. [Öneri] - [Nasıl daha iyi olabilirdi]
2. [Öneri]

---

## Ekstra Özellikler

Herhangi bir ekstra özellik yok.

---

## Dosya Yapısı

```
Assets/
│   ├── Scripts/
│   │   ├── Runtime/
│   │   │   ├── Core/
│   │   │   │   ├── IInteractable.cs
│   │   │   │   └── ...
│   │   │   ├── Interactables/
│   │   │   │   ├── Door.cs
│   │   │   │   └── ...
│   │   │   ├── Player/
│   │   │   │   └── ...
│   │   │   └── UI/
│   │   │       └── ...
│   │   └── Editor/
│   ├── ScriptableObjects/
│   ├── Prefabs/
│   ├── Materials/
│   └── Scenes/
│       └── TestScene.unity
├── Docs/
│   ├── CSharp_Coding_Conventions.md
│   ├── Naming_Convention_Kilavuzu.md
│   └── Prefab_Asset_Kurallari.md
├── README.md
├── PROMPTS.md
└── .gitignore
```

---

## İletişim

| Bilgi | Değer |
|-------|-------|
| Ad Soyad | [Serhan Turgul] |
| E-posta | [serhanturgul2@gmail.com] |
| LinkedIn | [https://www.linkedin.com/in/serhan-turgul/] |
| GitHub | [https://github.com/SerhanTRGL] |

---

*Bu proje Ludu Arts Unity Developer Intern Case için hazırlanmıştır.*
