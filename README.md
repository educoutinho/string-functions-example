# StringFunctionsExample
Função implementada em C# para remover acentuação e caracteres especifiais


## RemoveDiatrics()
Remove acentuação do texto

```
RemoveDiacritics("Maça") -> "Maca"
RemoveDiacritics("José da Silva") -> "Jose da Silva"  
RemoveDiacritics("Coração") -> "Coracao"  
```

## RemoveSpecialCharacters()
Remove caracteres especiais do texto

```
var text = "Você coloca maça no açaí ? @#$%¨&*";

RemoveSpecialCharacters(text, removeSpaces: false, removeDiacritics: false) -> "Você coloca maça no açaí"
RemoveSpecialCharacters(text, removeSpaces: true, removeDiacritics: false) -> "Vocêcolocamaçanoaçaí"
RemoveSpecialCharacters(text, removeSpaces: true, removeDiacritics: true) -> "Vocecolocamacanoacai"
```

---

Para mais informações, consulte:

https://educoutinho.com.br/csharp/como-remover-acentos-e-caracteres-especiais-de-uma-string-em-csharp/
