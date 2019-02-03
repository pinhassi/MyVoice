import csv
def hasNumbers(inputString):
    return any(char.isdigit() for char in inputString) and not '{' in inputString

def removeNikud(word):
    no_nikud = ''.join(x for x in word if x in ["א","ב","ג","ד","ה","ו","ז","ח","ט","י","כ","ל","מ","נ","ס","ע","פ","צ","ק","ר","ש","ת","ך","ף","ץ","ן","ם"])
    return no_nikud


out_contain_numbers_list = []
out_two_words_list = []
out_sentences_list = []
out_common_words = []
out_unsorted = []

orig_words_list = []

with open('words_list.txt') as fp:
    orig_words_list = fp.readlines()
    orig_words_list = [line[:-1] for line in orig_words_list]


for phrase in orig_words_list:
    if hasNumbers(phrase):
        out_contain_numbers_list.append(phrase)
    elif phrase.count(' ') == 1 or phrase.count('-') == 1:
        out_two_words_list.append(phrase)
    elif phrase.count(' ') >= 2 or phrase.count('-') >= 2:
        out_sentences_list.append(phrase)
    else:
        out_unsorted.append(phrase)


common_words_10k = []
with open('10000 מילים נפוצות בעברית.txt', mode='r') as csv_file:
    csv_reader = csv.DictReader(csv_file)
    line_count = 0
    for row in csv_reader:
        if line_count == 0:
            print('Column names are {}'.format(", ".join(row)))
            line_count += 1
        line_count += 1
        common_words_10k.append(row['word'])

out_unsorted_no_nikud_map = {}
for word in out_unsorted:
    no_nikud = removeNikud(word)
    no_nikud_list = out_unsorted_no_nikud_map.get(no_nikud,[])
    no_nikud_list.append(word)
    out_unsorted_no_nikud_map[no_nikud] = no_nikud_list

for common_word in common_words_10k:
    no_nikud_list_values = out_unsorted_no_nikud_map.pop(common_word,[])
    out_common_words.extend(no_nikud_list_values)
    for word in no_nikud_list_values:
        if word in out_unsorted: out_unsorted.remove(word)


def writeToFile(words_list, baseFilename, max_num_of_words = 100000):
    file_index = 1
    while (file_index-1) * max_num_of_words < len(words_list):
        filename = '{}_{}.txt'.format(baseFilename,file_index)
        with open(filename, mode='w') as the_file:
            start_index = (file_index-1) * max_num_of_words
            end_index = min((file_index * max_num_of_words),len(words_list))
            for i in range(start_index,end_index):
                the_file.write(words_list[i] + '\n')
        file_index +=1
 
   
writeToFile(out_contain_numbers_list,'out_contain_numbers_list',3000)   
writeToFile(out_two_words_list,'out_two_words_list',3000)
writeToFile(out_sentences_list,'out_sentences_list',3000)
writeToFile(out_common_words,'out_common_words',3000)
writeToFile(out_unsorted,'out_unsorted',3000)


print ('total words: {}'.format(len(out_contain_numbers_list) + len(out_two_words_list) + len(out_sentences_list) + len(out_common_words) + len(out_unsorted)))
